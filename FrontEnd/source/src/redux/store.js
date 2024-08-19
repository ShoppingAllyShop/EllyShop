import { configureStore } from "@reduxjs/toolkit";
import { thunk } from "redux-thunk";
import rootReducer from "./rootReducer.js";

// Cấu hình Redux store với redux-thunk
const store = configureStore({
  reducer: rootReducer, // Kết hợp các reducer
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(thunk),
});

export default store;