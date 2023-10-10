using Dapper;
using Npgsql;
namespace infrastructure.Repositories;

public class BoxRepository
{
    private NpgsqlDataSource _dataSource;

    public BoxRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<BoxFeedQuery> GetBoxFeed()
    {
        var query = "SELECT * FROM public.boxes";
        using (var conn = _dataSource.OpenConnection())
        { 
            return conn.Query<BoxFeedQuery>(query);
        }
    }


    public IEnumerable<BoxFeedQuery> GetBoxById(int boxId)
    {
        var query = "SELECT * FROM public.boxes WHERE boxId = @boxId";
        using (var conn = _dataSource.OpenConnection())
        {
            try
            {
                return conn.Query<BoxFeedQuery>(query, new { boxId = boxId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }


    public bool DeleteBoxById(int boxId)
    {
        var query = "DELETE FROM public.boxes WHERE BoxId = @boxId";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(query, new { boxId = boxId }) == 1;
        }
    }

    public BoxFeedQuery UpdateBoxById(int boxId, string boxName, string material, decimal width, decimal height, decimal depth)
    {
        var updateQuery = "UPDATE public.boxes " +
                          "SET boxName = @BoxName, material = @Material, " +
                          "width = @Width, height = @Height, depth = @Depth " +
                          "WHERE boxId = @BoxId " +
                          "RETURNING *"; // Add RETURNING * to get the updated row

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<BoxFeedQuery>(updateQuery, new { boxId, boxName, material, width, height, depth });
        }
    }


    public IEnumerable<BoxFeedQuery> CreateBox(string boxName, string material, decimal width, decimal height, decimal depth)
    {
        var insertQuery = "INSERT INTO public.boxes (boxName, material, width, height, depth) " +
                          "VALUES (@BoxName, @Material, @Width, @Height, @Depth) RETURNING boxId";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeedQuery>(insertQuery, new { boxName, material, width, height, depth });
        }
    }
}

public class BoxFeedQuery
{
    public int BoxId { get; set; }
    public string BoxName { get; set; }
    public string Material { get; set; }
    public decimal Width { get; set; } 
    public decimal Height { get; set; } 
    public decimal Depth { get; set; } 
}
