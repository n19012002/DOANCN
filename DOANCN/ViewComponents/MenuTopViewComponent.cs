using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOANCN.ViewComponents
{
    public class MenuTopViewComponent : ViewComponent
    {
        private readonly RenluyenContext _context;

        public MenuTopViewComponent(RenluyenContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.Menus.Where(m => (bool)m.IsActive).ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}