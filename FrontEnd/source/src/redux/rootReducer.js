import { combineReducers } from "redux";
import adminCategoryReducer from "./slices/admin/categorySlice";
import adminCollectionReducer from "./slices/admin/collectionSlice";
import adminLayoutReducer from "./slices/admin/adminLayoutSlice";
import clientLayoutReducer from "./slices/client/layoutSlice";
import selectedColorSlice from "./slices/client/selectedColorSlice";
import adminUserReducer from "./slices/admin/userSlice";
const rootReducer = combineReducers({
  //Client page
  clientLayout: clientLayoutReducer,
  selectedColor: selectedColorSlice,

  //Admin page
  adminCategory: adminCategoryReducer,
  adminLayout: adminLayoutReducer,
  adminUserPage: adminUserReducer,
  adminCollection: adminCollectionReducer,
});

export default rootReducer;