import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  user: {}
};

const adminLayoutSlice = createSlice({
  name: "adminLayout",
  initialState,
  reducers: {
    setUser: (state, action) => {
      console.log('action',action)
        state.user = action.payload
    }
  },
});

export const { setUser } = adminLayoutSlice.actions;

export default adminLayoutSlice.reducer;
