export interface BaseResponse<T> {
    isSuccess: boolean;
    data: T;
    message?: string;
    errors?: BaseError[];
  }
  
  export interface BaseError {
    propertyName?: string;
    errorMessage?: string;
  }
