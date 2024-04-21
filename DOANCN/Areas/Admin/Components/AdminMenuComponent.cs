using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOANCN.Areas.Admin.Components
{

    [ViewComponent(Name = "AdminMenu")]
    public class AdminMenuComponent : ViewComponent
    {
        private readonly RenluyenContext _context;

        public AdminMenuComponent(RenluyenContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.AdminMenus
                          where (mn.IsActive == true)
                          select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
