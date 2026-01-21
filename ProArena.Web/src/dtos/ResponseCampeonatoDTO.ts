export interface ResponseCampeonatoDTO {
    CampeonatoId: number,
    Descricao: string,
    DataFim: string,
    DataInicio: string,
    EquipeDTO : [{}]
    PartidaDTO : [{}]
}