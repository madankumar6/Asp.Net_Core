using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommad<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }

    public interface IQuery : ICommad<Unit>
    {
    }
}
