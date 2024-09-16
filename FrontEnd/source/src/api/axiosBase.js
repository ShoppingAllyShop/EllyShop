// import axios from 'axios';
// import { ACCESS_TOKEN_LOCAL_STORAGE_KEY } from '../constants/common';
// import { ADMIN } from '../constants/endpoint';
// import { useNavigate } from 'react-router-dom';


// const BASE_URL = process.env.REACT_APP_BASE_API_URL
// const axiosBase = axios.create({
//   baseURL: BASE_URL, // Thay bằng URL của API của bạn
//   timeout: 120000, // 2 minutes
//   headers: {
//     'Content-Type': 'application/json'
//   },
// });

// // Thiết lập interceptor cho response
// axiosBase.interceptors.response.use(
//   response => {
//       // Xử lý dữ liệu response nếu cần trước khi trả về
//       return response;
//   },
//   error => {
//       // Xử lý lỗi trong quá trình nhận response
//       HandleError(error);
//       return Promise.reject(error); // Reject để tiếp tục xử lý lỗi trong các Promise chain
//   }
// );

// // Hàm xử lý lỗi
// const HandleError = (error) => {
//   console.log('error', error)

//   if (error.response) {
//       // Server đã phản hồi nhưng với status ngoài dải 2xx
//       console.error('Server responded with a status:', error.response.status);
//       console.error('Response data:', error.response.data);

//       // Bạn có thể thêm các logic tùy chỉnh ở đây, ví dụ:
//       if (error.response.status === 401) {
//           alert('Unauthorized. Please log in again.');
//           // Có thể redirect đến trang đăng nhập hoặc xử lý refresh token
//       } else if (error.response.status === 403) {
//           alert('You do not have permission to perform this action.');
//       } else if (error.response.status === 500) {
//           alert('Internal server error. Please try again later.');
//       } else {
//           alert('An unexpected error occurred.');
//       }

//   } else if (error.request) {
//       // Request đã được gửi nhưng không có phản hồi
//       console.error('No response received:', error.request);
//       alert('No response from the server. Please check your network connection.');
//   } else {
//       // Có lỗi xảy ra trong quá trình thiết lập request
//       console.error('Error in setting up the request:', error.message);
//       alert('An error occurred while setting up the request.');
//   }
// }

// export default axiosBase;