using Microsoft.AspNetCore.Mvc;

public class RoomsController : Controller
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public async Task<IActionResult> Index()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        return View(rooms);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Room room)
    {
        if (ModelState.IsValid)
        {
            await _roomService.AddRoomAsync(room);
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        return View(room);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Room room)
    {
        if (id != room.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _roomService.UpdateRoomAsync(room);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists(room.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _roomService.DeleteRoomAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RoomExists(int id)
    {
        return await _roomService.RoomExistsAsync(id);
    }
}
