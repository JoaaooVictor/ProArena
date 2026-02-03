export function ValidaCampos(nome: string, cpf: string, email: string, senha: string, confirmaSenha: string) {
  if (!nome) return 'Nome obrigatório'
  if (!cpf) return 'CPF obrigatório'
  if (!email) return 'Email obrigatório'
  if (!senha) return 'Senha obrigatória'
  if (!confirmaSenha) return 'Confirmação obrigatória'
  return null
}
