using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Models.Plan;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Barber.Colocho.Core.Plan
{
    public class PlanBL
    {

        #region Constructor
        private readonly IGenericRepository<Entities.Tables.Plan> plan;
        private readonly ErrorBL errorBL;

        public PlanBL(IGenericRepository<Entities.Tables.Plan> plan, ErrorBL errorBL)
        {
            this.plan = plan;
            this.errorBL = errorBL;
        }
        #endregion

        public async Task<Response<List<PlanModel>>> ListPlan()
        {
            try
            {
                var list = new List<PlanModel>();
                var search = await plan.GetAllAsync(c => c.IsActive, include: query => query.Include(s => s.Suscription).ThenInclude(c => c.Company));
                foreach (var item in search) 
                {
                    int countSuscription = item.Suscription.Where(s => s.IsActive).Count();
                    if (item.IsFree)
                    {
                        if(countSuscription < item.QuantityUser)
                        {
                            list.Add(new PlanModel
                            {
                                Price = item.Price,
                                Name = item.Name,
                                Description = item.Description,
                                Id = item.Id,
                                IsFree = item.IsFree,
                                SKU = item.SKU,
                                QuantityUser = item.QuantityUser - countSuscription
                            });
                        }
                    }
                    else
                    {
                        list.Add(new PlanModel
                        {
                            Price = item.Price,
                            Name = item.Name,
                            Description = item.Description,
                            Id = item.Id,
                            IsFree = false,
                            SKU = item.SKU,
                            QuantityUser = 0
                        });
                    }
                }
                return new Response<List<PlanModel>>
                {
                    Result = list,
                    Count = list.Count
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<PlanModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente, más tarde"
                };
            }
        }
    }
}
