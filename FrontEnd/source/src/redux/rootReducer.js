import { combineReducers } from "redux";
import adminCategoryReducer from "./slices/admin/categorySlice";
import adminLayoutReducer from "./slices/admin/adminLayoutSlice"
const rootReducer = combineReducers({
  //Client page

  //Admin page
  adminCategory: adminCategoryReducer,
  adminLayout: adminLayoutReducer
});

export default rootReducer;