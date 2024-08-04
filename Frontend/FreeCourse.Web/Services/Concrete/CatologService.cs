using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace FreeCourse.Web.Services.Concrete
{
    public class CatologService : ICatologService
    {
        private readonly HttpClient _httpClient;

        public CatologService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
         
        public async Task<bool> CreateCourseAsync(CreateCourseViewModel createCourseViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateCourseViewModel>("courses", createCourseViewModel);

            if(response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return data.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"Courses/GetByUserIdCourse/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return data.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {   
            // BaseUrl program.cs ' te tanımlandı.
            // http://localhost:5000/services/catolog/

            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            var data = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();

            return data.ToList();

        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return data.Data;
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseViewModel updateCourseViewModel)
        {
            var response = await _httpClient.PutAsJsonAsync<UpdateCourseViewModel>("courses", updateCourseViewModel);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
