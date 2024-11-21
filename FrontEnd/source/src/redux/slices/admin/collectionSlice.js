import { createSlice } from "@reduxjs/toolkit";
import { addItem, updatedCollection } from "../../business/admin/collectionSlice";

const initialState = {  
  data: {collectionData:{paging:{}, collectionList:[]}},
  loading: false,
  error: null,
  selectedCollection: [],
  searchInput:'',
  createUpdateCollectionModal: {isShow: false, type: '' }, //nút create và edit category xài chung 1 modal
  alert: {isDisplay:false, content:'', type:''}
};

const collectionSlice = createSlice({
  name: "adminCollection",
  initialState,
  reducers: {
    setData: (state, action) => {
      state.data = action.payload
    },
    setCreateUpdateCollectionModal: (state, action) => {
        state.createUpdateCollectionModal = action.payload
    },
    addNewItemData:(state, action) => {
      addItem(state, action.payload)
    },
    updateCollection:(state, action) => {
      updatedCollection(state.data.collectionData.collectionList, action.payload)
    },
    deleteCollection: (state, action) => {
      state.data.collectionData.collectionList = action.payload     
    },
    setCollectionData:(state, action) => {
      state.data.collectionData = action.payload      
    },
    setAlert: (state, action) => {
      state.alert = action.payload
    },
    setSearchInput: (state, action) => {
      state.searchInput = action.payload
    },
  },
});

export const { setData, setAlert, setCreateUpdateCollectionModal, setCollectionData,
   deleteCollection, updateCollection , addNewItemData,setSearchInput} = collectionSlice.actions;

export default collectionSlice.reducer;
