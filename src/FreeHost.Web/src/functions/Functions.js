export const imageToBase64 = (file) => {
    var reader = new FileReader();
    return new Promise((resolve, reject) => {
      reader.onload = () => {
        var base64String = reader.result
          .replace("data:", "")
          .replace(/^.+,/, "");
        resolve(base64String);
      };
      reader.onerror = () => {
        reject("oops, something went wrong with the file reader.");
      };
      reader.readAsDataURL(file);
    });
  };