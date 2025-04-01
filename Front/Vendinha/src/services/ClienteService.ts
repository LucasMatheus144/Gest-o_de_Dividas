import { Cliente } from "../types/Clientes";

export async function GetListagem(
  search: string = '',
  page: number = 0,
  size: number = 6
): Promise<Cliente[]> {
  const baseUrl = "https://localhost:7083/api/Cliente";
  const url = `${baseUrl}?page=${page}&size=${size}${search ? `&Nome=${encodeURIComponent(search)}` : ''}`;

  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Erro na API: ${response.statusText}`);
    }
    return await response.json() as Cliente[];
  } catch (error) {
    console.error("Erro ao buscar clientes:", error);
    return [];
  }
}
