import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  data: [
    {
      id: "B3710F84-4B55-4676-8F9A-919FCEC64C9F",
      parentId: null,
      name: "Túi Xách",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "7bd78cfa-6b0f-4007-9956-088e76c97ef6",
      parentId: "b3710f84-4b55-4676-8f9a-919fcec64c9f",
      name: "Túi Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "9f33bc97-27b3-4612-9f32-ecf057b5c556",
      parentId: "b3710f84-4b55-4676-8f9a-919fcec64c9f",
      name: "Túi Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "dce80194-7e6b-49ee-aeb5-eb93213e10a3",
      parentId: "b3710f84-4b55-4676-8f9a-919fcec64c9f",
      name: "Túi Xách Thời Trang",
      categoryLevel: 2,
      gender: null,
    },
    {
      id: "4bbedd5b-dbc2-4a07-a6ce-3e251af6e4d9",
      parentId: "b3710f84-4b55-4676-8f9a-919fcec64c9f",
      name: "Túi Xách Da Thật ",
      categoryLevel: 2,
      gender: null,
    },
    {
      id: "749602d7-2e1b-4b69-9dfb-93e46123d3be",
      parentId: null,
      name: "Ví Da",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "f7dc3f24-373a-417c-b31d-dfe7ec689740",
      parentId: "749602d7-2e1b-4b69-9dfb-93e46123d3be",
      name: "Ví Da Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "6372a5aa-d07d-4c26-a67e-d83c93fb37c5",
      parentId: "749602d7-2e1b-4b69-9dfb-93e46123d3be",
      name: "Ví Da Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "0bf1175d-f1fc-4953-bcd1-d1bf34a676c3",
      parentId: null,
      name: "Clutch Cầm Tay",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "fe3931bf-7f11-46c8-b5c2-10fad7116e7a",
      parentId: "0bf1175d-f1fc-4953-bcd1-d1bf34a676c3",
      name: "Clutch Cầm Tay Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "a8940555-6b14-4ac8-a1d1-04686f3e033f",
      parentId: "0bf1175d-f1fc-4953-bcd1-d1bf34a676c3",
      name: "Clutch Cầm Tay Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "244c9d82-e421-4cdc-a889-61de88c7d260",
      parentId: null,
      name: "Balo",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "bd9d96e9-a749-4122-b072-e85d2b55158e",
      parentId: "244c9d82-e421-4cdc-a889-61de88c7d260",
      name: "Balo Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "f4b0b5c2-1f93-4309-87e5-7142f0e0a94c",
      parentId: "244c9d82-e421-4cdc-a889-61de88c7d260",
      name: "Balo Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "1c64e7b0-8885-49d7-8849-deaa314be84e",
      parentId: null,
      name: "Giày Dép",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "aa1d2589-96cf-43ca-bf40-7fda4db3a79d",
      parentId: "1c64e7b0-8885-49d7-8849-deaa314be84e",
      name: "Giày Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "2ca51b89-9e58-4798-ac49-702a4a1a110b",
      parentId: "1c64e7b0-8885-49d7-8849-deaa314be84e",
      name: "Sandal Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "18e089cc-6b8b-45a9-b820-75bda5f59a22",
      parentId: "1c64e7b0-8885-49d7-8849-deaa314be84e",
      name: "Giày Da Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "07b971ac-c213-443c-95a2-55d10dc4d12a",
      parentId: null,
      name: "Kính mắt",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "ac770a8c-b85d-4044-b9fa-0e65789a7036",
      parentId: "07b971ac-c213-443c-95a2-55d10dc4d12a",
      name: "Kính Mắt Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "bd3e756b-1bf8-410c-b3c3-5400209532b0",
      parentId: "07b971ac-c213-443c-95a2-55d10dc4d12a",
      name: "Kính Mắt Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "332c7a85-ac16-4ba7-82dc-277dbfddb3c0",
      parentId: null,
      name: "Thắt Lưng",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "4926590c-b5dc-434d-a1b0-c45fe281bfdd",
      parentId: "332c7a85-ac16-4ba7-82dc-277dbfddb3c0",
      name: "Thắt Lưng Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "81a6940e-d55e-421e-aec6-d8502f8e8abe",
      parentId: "332c7a85-ac16-4ba7-82dc-277dbfddb3c0",
      name: "Thắt Lưng Nam",
      categoryLevel: 2,
      gender: true,
    },
    {
      id: "f9f06d59-d7d7-4239-8c25-e6f6ab9e89f7",
      parentId: null,
      name: "Đồng Hồ",
      categoryLevel: 1,
      gender: null,
    },
    {
      id: "691cba3d-9466-4119-b210-afdbeeb25fce",
      parentId: "f9f06d59-d7d7-4239-8c25-e6f6ab9e89f7",
      name: "Đồng Hồ Nữ",
      categoryLevel: 2,
      gender: false,
    },
    {
      id: "f5a6959e-3d94-4d71-b440-f470edb7c144",
      parentId: null,
      name: "Phiếu Quà Tặng",
      categoryLevel: 1,
    },
    {
      id: "1dd2bb19-79eb-4b29-be00-0f26bf83879c",
      parentId: "f5a6959e-3d94-4d71-b440-f470edb7c144",
      name: "Phiếu 500.000 VNĐ",
      categoryLevel: 2,
    },
    {
      id: "65b16b28-eb9a-4df1-b2fb-2871aca48805",
      parentId: "f5a6959e-3d94-4d71-b440-f470edb7c144",
      name: "Phiếu 1.000.000 VNĐ",
      categoryLevel: 2,
    },
  ],
  loading: false,
  error: null,
  selectedCategory: [],
  createEditModal: { level: null, isDisplay: false, type: "" }, //nút create và edit category xài chung 1 modal
};

const categorySlice = createSlice({
  name: "adminCategory",
  initialState,
  reducers: {
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
      console.log('state', state.data)
      state.data = [...state.data, action.payload];
      state.createEditModal = { level: null, isDisplay: false };
    },
    triggerEditModal: (state, action) => {
      state.createEditModal = action.payload;
    },
  },
});

export const { setCreateCateButton, createNewCate, setSelectedCategory } =
  categorySlice.actions;

export default categorySlice.reducer;
