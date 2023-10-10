using System.Collections;
using infrastructure.Repositories;
namespace service;

public class BoxService
{
    private readonly BoxRepository _boxRepository;
    public BoxService(BoxRepository boxRepository)
    {
        _boxRepository = boxRepository;
    }

    public IEnumerable<BoxFeedQuery> GetBoxFeed()
    {
        return _boxRepository.GetBoxFeed();
    }

    public IEnumerable<BoxFeedQuery> GetBoxById(int boxId)
    {
        return _boxRepository.GetBoxById(boxId);
    }

    public void DeleteBoxById(int boxId)
    {
        var result = _boxRepository.DeleteBoxById(boxId);
        if (!result)
        {
            throw new ArgumentException("Could not delete Box");
        }
    }

    public BoxFeedQuery UpdateBoxById(int boxId, string BoxName, string Material, decimal Width, decimal Height,
        decimal Depth)
    {
        return _boxRepository.UpdateBoxById(boxId, BoxName, Material, Width, Height, Depth);
    }

    public IEnumerable<BoxFeedQuery> CreateBox(string BoxName, string Material, decimal Width, decimal Height,
        decimal Depth)
    {
        return _boxRepository.CreateBox( BoxName,  Material,  Width,  Height, Depth);
    }


}
