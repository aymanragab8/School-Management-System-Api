
namespace SchoolProject.Application.Bases
{
    public class ResponseHandler
    {
        public ResponseHandler()
        {

        }
        public Response<T> Deleted<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully"
            };
        }
        public Response<T> Updated<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Updated Successfully"
            };
        }
        public Response<T> Unauthorized<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = Message == null ? "UnAuthorized" : Message
            };
        }
        public Response<T> GetData<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Get Data Successfully"
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }
        public Response<T> Registered<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = Message == null ? "Registered Successfully" : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }

        public Response<T> Created<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created"
            };
        }
        public Response<T> Success<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Success"
            };
        }
    }

}
