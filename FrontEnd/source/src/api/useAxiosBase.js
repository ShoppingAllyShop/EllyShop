import axios from "axios";
import { useNavigate } from "react-router-dom";
import { ADMIN_ENDPOINT } from "../constants/endpoint";
import { RESPONSE_API_STATUS } from "../constants/common";

const useAxiosBase = () => {
  const BASE_URL = process.env.REACT_APP_BASE_API_URL;
  const nagative = useNavigate()
  const axiosBase = axios.create({
    baseURL: BASE_URL, // Thay bằng URL của API của bạn
    timeout: 120000, // 2 minutes
    headers: {
      "Content-Type": "application/json",
    },
  });

   //Thiết lập interceptor cho request
  // axiosBase.interceptors.request.use(    
  //   (error) => {
  //     nagative(ADMIN.ERROR, {state: {errorHeader:'Server đang gặp trục trặc kỹ thuật'}})
  //     Promise.reject(error)
  //   } 
  // );

  //Thiết lập interceptor cho response
  axiosBase.interceptors.response.use(
    (response) => {
      // Xử lý dữ liệu response nếu cần trước khi trả về
      return response;
    },
    (error) => {      
      // Xử lý lỗi trong quá trình nhận response
      if(error.code === RESPONSE_API_STATUS.ERROR_NETWORK || error.response.status === 500){
        nagative(ADMIN_ENDPOINT.ERROR, {state: {errorHeader:'Server đang gặp trục trặc kỹ thuật'}})
      }
      //handleError(error);
      return Promise.reject(error); // truyền error lại component
    }
  );

  // // Hàm xử lý lỗi
  // const handleError = (error) => {
  //   console.log('mmmmmmmmmm', error)
  //   //nagative(ADMIN.ERROR)

  //   if (error.response) {
  //     // Server đã phản hồi nhưng với status ngoài dải 2xx
  //     console.error("Server responded with a status:", error.response.status);
  //     console.error("Response data:", error.response.data);

  //     // Bạn có thể thêm các logic tùy chỉnh ở đây, ví dụ:
  //     if (error.response.status === 401) {
  //       console.log('dmm', error)
  //       //alert("Unauthorized. Please log in again.");
  //       // Có thể redirect đến trang đăng nhập hoặc xử lý refresh token
  //     } else if (error.response.status === 403) {
  //       alert("You do not have permission to perform this action.");
  //     } else if (error.response.status === 500) {
  //       alert("Internal server error. Please try again later.");
  //     } else {
  //       alert("An unexpected error occurred.");
  //     }
  //   } else if (error.request) {
  //     // Request đã được gửi nhưng không có phản hồi      
  //     console.error("No response received:", error.request);
  //     //nagative(ADMIN.ERROR)
  //     // alert(
  //     //   "No response from the server. Please check your network connection."
  //     // );
  //   } else {
  //     // Có lỗi xảy ra trong quá trình thiết lập request
  //     console.error("Error in setting up the request:", error.message);
  //      nagative(ADMIN_ENDPOINT.ERROR)
  //     //alert("An error occurred while setting up the request.");
  //   }
  // };
  return axiosBase;
};

export default useAxiosBase;
