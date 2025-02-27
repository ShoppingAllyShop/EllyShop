import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  data: {}
};

const selectedColorSlice = createSlice({
  name: "SelectedColor",
  initialState,
  reducers: {
    setSelectedColor: (state, action) => {
        state.data = action.payload
    }
  },
});

export const { setSelectedColor } = selectedColorSlice.actions;

export default selectedColorSlice.reducer;
