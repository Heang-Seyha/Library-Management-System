using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Generic abstract base service for entity CRUD operations against LibraryDbContext.
    /// Provides reusable GetAll and GetById operations for standard entities.
    /// </summary>
    /// <typeparam name="TEntity">The EF Core entity type.</typeparam>
    public abstract class CrudService<TEntity> where TEntity : class
    {
        protected readonly LibraryDbContext _context;

        protected CrudService(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all entities from the database.
        /// </summary>
        public virtual List<TEntity> GetAll() =>
            _context.Set<TEntity>().ToList();

        /// <summary>
        /// Finds an entity by primary key ID.
        /// </summary>
        public virtual TEntity? GetById(int id) =>
            _context.Set<TEntity>().Find(id);
    }
}
