namespace RadzenBlazorInlineEditingBug.Components.Pages;

using Radzen;
using Radzen.Blazor;
using RadzenBlazorInlineEditingBug.Data;
using RadzenBlazorInlineEditingBug.ViewModels;
using System.Linq;

public partial class Index
{
    bool allowPaging;
    bool allowVirtualization;
    IQueryable<MyMainViewModel> data;
    List<Category> categories;
    RadzenDataGrid<MyMainViewModel> dataGrid;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _context.Database.EnsureCreated();
        categories = _context.Categories.ToList();
        data = _context.Objects.Join(_context.Categories, o => o.CategoryId, c => c.Id, (o, c) => 
        new MyMainViewModel 
        {
            Name= o.Name,
            Id = o.Id,
            Description = o.Description,
            CategoryId = o.CategoryId,
            CategoryName = c.Name
        }).AsQueryable();
        var showBug = true; //change this to false and the problem goes away with paging.  Requires restart.
        if (showBug)
        {
            allowPaging = false;
            allowVirtualization = true;
        }
        else
        {
            allowPaging = true;
            allowVirtualization = false;
        }
       
    }

    void OnUpdateRow(MyMainViewModel obj)
    {
        //update logic goes here
    }

    async Task EditRow(MyMainViewModel obj)
    {

        await dataGrid.EditRow(obj);
    }

    async Task SaveRow(MyMainViewModel obj)
    {
        await dataGrid.UpdateRow(obj);
    }

    void CancelEdit(MyMainViewModel obj)
    {

        dataGrid.CancelEditRow(obj);
    }

    private static void RowRender(RowRenderEventArgs<MyMainViewModel> args)
    {

    }
}