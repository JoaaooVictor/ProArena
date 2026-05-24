export function AplicaMascaraCpf(cpf: string) {
    return cpf
        .replace(/\D/g, '')
        .replace(/(\d{3})(\d)/, '$1.$2')
        .replace(/(\d{3})(\d)/, '$1.$2')
        .replace(/(\d{3})(\d{1,2})$/, '$1-$2')
        .slice(0, 14)
}

export function FormataData(data?: string) {
    if (!data) return '-';
    return new Date(data).toLocaleDateString('pt-BR');
}

export function FormataMoeda(valor: number) {
    return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
}