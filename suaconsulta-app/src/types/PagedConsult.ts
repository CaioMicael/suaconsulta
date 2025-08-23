/**
 * Type padrão para consultas paginadas.
 */
export interface PagedConsult<T> {
    items:Array<T>;
    pageNumber:Number;
    pageSize:Number;
    totalCount:Number;
    totalPages:Number;
}