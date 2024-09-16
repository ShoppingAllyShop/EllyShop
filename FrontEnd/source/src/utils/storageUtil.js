import { STORAGE_TYPE } from "../constants/common";

const storageUtil = {
    setItem: (key, value, storageType = STORAGE_TYPE.LOCAL) => {
      try {
        const serializedValue = JSON.stringify(value);
        if (storageType === STORAGE_TYPE.LOCAL) {
          localStorage.setItem(key, serializedValue);
        } else if (storageType === STORAGE_TYPE.SESSION) {
          sessionStorage.setItem(key, serializedValue);
        } else {
          throw new Error("Invalid storage type specified");
        }
      } catch (error) {
        console.error("Failed to save in storage:", error);
      }
    },
  
    getItem: (key, storageType = STORAGE_TYPE.LOCAL) => {
      try {
        const serializedValue =
          storageType === STORAGE_TYPE.LOCAL
            ? localStorage.getItem(key)
            : sessionStorage.getItem(key);
  
        if (serializedValue === null) return null;
  
        return JSON.parse(serializedValue);
      } catch (error) {
        console.error("Failed to retrieve from storage:", error);
        return null;
      }
    },
  
    removeItem: (key, storageType = STORAGE_TYPE.LOCAL) => {
      try {
        if (storageType === STORAGE_TYPE.LOCAL) {
          localStorage.removeItem(key);
        } else if (storageType === STORAGE_TYPE.SESSION) {
          sessionStorage.removeItem(key);
        } else {
          throw new Error("Invalid storage type specified");
        }
      } catch (error) {
        console.error("Failed to remove from storage:", error);
      }
    },
  
    getItemProp: (key, propPath, storageType = STORAGE_TYPE.LOCAL) => {
        try {
          const item = storageUtil.getItem(key, storageType);
          if (item && typeof item === 'object' && item !== null) {
            // Split propPath into an array of properties
            const props = propPath.split('.');
            let result = item;
            for (const prop of props) {
              if (result && typeof result === 'object') {
                result = result[prop];
              } else {
                return null;
              }
            }
            return result;
          }
          return null;
        } catch (error) {
          console.error("Failed to retrieve property from storage:", error);
          return null;
        }
      },

      updateItemProp: (key, propPath, value, storageType = STORAGE_TYPE.LOCAL) => {
        try {
          const item = storageUtil.getItem(key, storageType);
          if (item && typeof item === 'object' && item !== null) {
            const props = propPath.split('.');
            let current = item;
    
            for (let i = 0; i < props.length - 1; i++) {
              const prop = props[i];
              if (!(prop in current)) {
                current[prop] = {}; // Tạo đối tượng mới nếu không tồn tại
              }
              current = current[prop];
            }
    
            current[props[props.length - 1]] = value;
    
            storageUtil.setItem(key, item, storageType);
          } else {
            console.error("Item not found or is not an object");
          }
        } catch (error) {
          console.error("Failed to update property in storage:", error);
        }
      },
  };
  
  export default storageUtil;