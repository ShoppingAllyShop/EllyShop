export const USER_FORM_CONST = {
    VALIDATION_ERRORS: {
      REQUIRED: "Bắt buộc không để trống",
      SELECTION_REQUIRED: "Vui lòng chọn",
      PASSWORD: "Mật khẩu phải trên 6 ký tự",
      PASSWORD_RULES:"Mât khẩu phải gồm chữ hoa, chữ thường và số",
      PASSWORD_MISMATCH: "Mật khẩu không giống"
    },
    MESSAGES: {
      NOT_CHANGED: "Bạn chưa thay đổi thông tin",
      UPDATE_SUCCESS: (userName) => `Tài khoản ${userName} cập nhật thành công`,
      CREATE_SUCCESS: "Tạo tài khoản mới thành công",
      DELETE_CONFIRM: (userName) => `Bạn có chắc muốn xóa tài khoản ${userName}?`,
      DELETE_SUCCESS: (userName) => `Tài khoản  ${userName} xóa thành công`
    },
    LABELS: {
      USERNAME: "Tên",
      PASSWORD: "Mật khẩu",
      REPASSWORD: "Xác nhận mật khẩu",
      EMAIL: "Email",
      ROLE: "Vai trò",
      OPTIONS: "Lựa chọn"
    },
    TITLES: {
      CREATE: "TẠO TÀI KHOẢN",
      UPDATE: "CẬP NHẬT TÀI KHOẢN"
    }
};

export const LOGIN_FORM_CONST = {
  VALIDATION_ERRORS: {
    REQUIRED: "Bắt buộc không để trống",
    REQUIRED_EMAIL: "Email không hợp lệ"
  },
  LABELS: {
    PASSWORD: "Mật khẩu",
    EMAIL: "Email",
    FORGET_PASSWORD: "Quên mật khẩu?",
    LOGIN: "Đăng nhập"
  }
};