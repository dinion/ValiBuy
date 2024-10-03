using Application.Common.Interfaces;

namespace Application.Items.Commands.CreateItem
{
    /// <summary>
    /// Represents a command to update an item by its ID.
    /// </summary>
    public record UpdateItemCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the ID of the item to be updated.
        /// </summary>
        public int ItemId { get; set; }
    }

    /// <summary>
    /// Handler for the <see cref="UpdateItemCommand"/>.
    /// </summary>
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access.</param>
        public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the update of an item by its ID.
        /// </summary>
        /// <param name="request">The request containing the ID of the item to be updated.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Items
                .GetByIdAsync(request.ItemId, cancellationToken);

            Guard.Against.NotFound(request.ItemId, entity);

            await _unitOfWork.CompleteAsync();
        }
    }
}