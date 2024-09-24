import axios from "axios";

const baseURL = "https://localhost:7135/api/user";

export const getApi = async (endpoint, params = {}) => {
  try {
    const response = await axios.get(`${baseURL}${endpoint}`, {
      params: params,
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    });
    return response.data;
  } catch (error) {
    console.error("There was an error fetching the data!", error);
    throw error;
  }
};

export const postApi = async (endpoint, data) => {
  try {
    const response = await axios.post(`${baseURL}/${endpoint}`, data, {
      headers: {
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    console.error("There was an error posting the data!", error);
    throw error;
  }
};

export const checkIsAdminPage = () => {
    return window.location.pathname.toLowerCase().includes("admin");
};
