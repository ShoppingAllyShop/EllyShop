import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  data: {}
};

const layoutSlice = createSlice({
  name: "Layout",
  initialState,
  reducers: {
    setData: (state, action) => {
        state.data = action.payload
    }
  },
});

export const { setData } = layoutSlice.actions;

export default layoutSlice.reducer;
