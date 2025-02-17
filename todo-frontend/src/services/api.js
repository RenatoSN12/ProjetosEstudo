const API_URL = 'https://seuservidor/api/todos';

export const fetchTodos = async () => {
  const response = await fetch(API_URL);
  const data = await response.json();
  return data;
};

export const addTodo = async (todo) => {
  const response = await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(todo),
  });
  const data = await response.json();
  return data;
};