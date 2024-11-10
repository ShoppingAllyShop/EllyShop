import React from "react";

const RefundPolicy = () => {
  return (
    <div
      className="p-4 bg-gray-50 rounded-lg dark:bg-gray-800"
      id="contacts"
      role="tabpanel"
    >
      <ul className="list-disc">
        <li className="mb-2">
          Quý khách vui lòng xuất trình hóa đơn mua hàng khi có yêu cầu đổi
          hàng.
        </li>
        <li className="mb-2">
          Chúng tôi không chấp nhận việc trả hàng để lấy tiền (TIỀN MẶT HAY TRỪ
          VÀO THẺ TÍN DỤNG)
        </li>
        <li className="mb-2">
          Trong bất kỳ tình huống nào, ELLY chỉ chấp nhận việc đổi hàng còn mới,
          chưa qua sử dụng, trong vòng 3 ngày kể từ ngày mua hàng.
        </li>
        <li className="mb-2">
          Hàng được đổi vì lý do kỹ thuật phải có sự đồng ý và xác nhận của
          ELLY.
        </li>
        <li className="mb-2">
          Hàng đổi phải còn mới và giữ nguyên tình trạng ban đầu, còn nguyên
          tem, nhãn mác.
        </li>
        <li className="mb-2">
          Hàng chưa qua sử dụng, chưa giặt và không bị hư hại. Hàng không bị
          bẩn, không dính các vết mỹ phẩm, không nhiễm mùi như nước hoa, kem cạo
          râu, chất khử mùi cơ thể hay khói thuốc lá, v.v…
        </li>
        <li className="mb-2">
          Hàng bán trong thời gian khuyến mãi, hoặc hàng đã được đổi một lần
          trước đó sẽ không áp dụng chính sách đổi trả hàng, trừ trường hợp do
          lỗi kỹ thuật.
        </li>
        <li className="mb-2">
          Quý khách vui lòng thanh toán cước phí chuyển phát nhanh khi đổi hàng.
        </li>
      </ul>
    </div>
  );
};

export default RefundPolicy;
