using DoctorWho.Db;
using DoctorWho.Db.Services;

namespace DoctorWho
{
    public class Program
    {
        static void Main(string[] args)
        {

            DoctorWhoCoreDbContext dbContext = new DoctorWhoCoreDbContext();

            var authorService = new EntityService<Author>(dbContext);
            var companionService = new EntityService<Companion>(dbContext);
            var enemyService = new EntityService<Enemy>(dbContext);
            var doctorService = new EntityService<Doctor>(dbContext);
            var episodeService = new EntityService<Episode>(dbContext);

            var operationService = new OperationService(dbContext);

            var callService = new CallService(dbContext);

            Console.ReadLine();


        }

    }
}