public interface ICamereService
{
    Task<IEnumerable<Camera>> GetAllCamereAsync();
    Task<Camera> GetCameraByIdAsync(int id);
    Task AddCameraAsync(Camera camera);
    Task UpdateCameraAsync(Camera camera);
    Task DeleteCameraAsync(int id);
    Task<bool> CameraExistsAsync(int id);
}
