//dung cho chuc nang tim kiem typing thi se delay khoang thoi gian moi call API
export const debounce = (func, delay) => {
  let timeout;
  return (...args) => {
    if (timeout) clearTimeout(timeout);
    timeout = setTimeout(() => {
      func(...args);
    }, delay);
  };
};
