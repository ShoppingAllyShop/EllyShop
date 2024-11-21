export const COLLECTION_FORM_CONST = {
    VALIDATION_ERRORS: {
      REQUIRED: "Bắt buộc không để trống",
      EXITED_NAME:"Tên bộ sưu tập này đã có vui lòng chọn tên khác"
    },
    MESSAGES: {
      NOT_CHANGED: "Bạn chưa thay đổi thông tin",
      UPDATE_SUCCESS: (userName) => `Tài khoản ${userName} cập nhật thành công`,
      CREATE_SUCCESS: "Tạo bộ sưu tập mới thành công",
      DELETE_CONFIRM: (userName) => `Bạn có chắc muốn xóa tài khoản ${userName}?`,
      DELETE_SUCCESS: (userName) => `bộ sưu tập  ${userName} xóa thành công`
    },
    LABELS: {
      USERNAME: "Tên",
      EMAIL: "Mô Tả",
      ROLE: "Vai trò",
      OPTIONS: "Lựa chọn"
    },
    TITLES: {
      CREATE: "TẠO BỘ SƯU TẬP",
      UPDATE: "CẬP NHẬT BỘ SƯU TẬP"
    }
};
