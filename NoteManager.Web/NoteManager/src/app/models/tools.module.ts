export interface ResponseModel<T> {
    count: number,
    data: T,
    status: number,
    info: string,
    totalCount: number
}