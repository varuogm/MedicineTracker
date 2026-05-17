const BASE = '/api';

async function handleResponse(res) {
  if (!res.ok) {
    const body = await res.json().catch(() => ({}));
    throw new Error(body.message || 'Something went wrong');
  }
  if (res.status === 204) return null;
  return res.json();
}

export function getMedicines(search) {
  const url = search ? `${BASE}/medicines?search=${search}` : `${BASE}/medicines`;
  return fetch(url).then(handleResponse);
}

export function addMedicine(data) {
  return fetch(`${BASE}/medicines`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  }).then(handleResponse);
}

export function updateMedicine(id, data) {
  return fetch(`${BASE}/medicines/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  }).then(handleResponse);
}

export function deleteMedicine(id) {
  return fetch(`${BASE}/medicines/${id}`, { method: 'DELETE' }).then(handleResponse);
}

export function getSales() {
  return fetch(`${BASE}/sales`).then(handleResponse);
}

export function addSale(data) {
  return fetch(`${BASE}/sales`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  }).then(handleResponse);
}
