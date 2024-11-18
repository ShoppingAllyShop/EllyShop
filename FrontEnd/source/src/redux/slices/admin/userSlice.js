import { createSlice } from "@reduxjs/toolkit";
import { addItem, updateItem } from "../../business/admin/userSlice";

const initialState = {
  data:{},
  createUpdateUserModal: {isShow: false, type:''},
  alert: {isDisplay: false, content:'' , type: 0 },
  searchInput:''
};

const adminUserSlice = createSlice({
  name: "AdminUserPage",
  initialState,
  reducers: {
    setData:(state, action) => {
      state.data = action.payload
    },    
    updateItemData:(state, action) => {
      updateItem(state.data.userData.userList, action.payload)
    },
    addNewItemData:(state, action) => {
      addItem(state, action.payload)
    },
    setCreateUpdateUserModal: (state, action) => {
        state.createUpdateUserModal = action.payload
    },
    setAlert: (state, action) => {
        state.alert = action.payload
    },
    setSearchInput: (state, action) => {
      state.searchInput = action.payload
    },
    setUserList:(state, action) => {
      state.data.userData.userList = action.payload      
    },
    setUserData:(state, action) => {
      state.data.userData = action.payload      
    },
  },
});

export const { setCreateUpdateUserModal, setAlert, setData, setUserData,
           updateItemData, addNewItemData, setUserList, setSearchInput } = adminUserSlice.actions;

export default adminUserSlice.reducer;
