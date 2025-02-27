import { createSlice } from "@reduxjs/toolkit";

const initialState = {  
  data: [],
  loading: false,
  error: null,
  selectedCategory: [],
  createEditModal: { level: null, isDisplay: false, type: '' }, //nút create và edit category xài chung 1 modal
  alert: {isDisplay:false, content:'', type:''}
};

const categorySlice = createSlice({
  name: "adminCategory",
  initialState,
  reducers: {
    setData: (state, action) => {
      state.data = action.payload
    },
    setSelectedCategory: (state, action) => {
      const index = state.selectedCategory.findIndex(
        (item) => item.categoryLevel === action.payload.categoryLevel
      );
      //remove all selected item when user select level 1
      if (action.payload.categoryLevel === 1) {
        state.selectedCategory = [
          ...state.selectedCategory.filter((item) => item.categoryLevel === 10),
        ];
      }

      //Add item to state
      if (index === -1) {
        state.selectedCategory = [...state.selectedCategory, action.payload];
      } else {
        state.selectedCategory[index] = action.payload;
      }
    },
    setCreateCateButton: (state, action) => {
      state.createEditModal = action.payload;
    },
    createNewCate: (state, action) => {
      state.data.push(action.payload)
      state.createEditModal = { level: null, isDisplay: false };
    },
    deleteCategory: (state, action) => {
      state.data = state.data.filter(item => item.id !== action.payload.id);
    },
    updateCategory:(state, action) => {
      const itemIndex = state.data.findIndex(item => item.id === action.payload.id); // Tìm index của item
      if (itemIndex !== -1) {
        // Cập nhật item dựa trên index tìm được
        state.data[itemIndex] = {
          ...state.data[itemIndex], // giữ lại các thuộc tính cũ
          ...action.payload // cập nhật các thuộc tính mới
        };
      }
    },
    triggerEditModal: (state, action) => {
      state.createEditModal = action.payload;
    },
    setAlert: (state, action) => {
      state.alert = action.payload
    }
  },
});

export const { setData, setAlert, setCreateCateButton, createNewCate, setSelectedCategory,
   triggerEditModal, deleteCategory, updateCategory } = categorySlice.actions;

export default categorySlice.reducer;
