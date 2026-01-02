import axios from "axios";
console.log("API BASE URL:", import.meta.env.VITE_API_BASE_URL);
const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});

export const getIncidents = async () => {
  const response = await api.get("/api/incidents");
  return response.data || [];
};

export const createIncident = async (payload) => {
  const response = await api.post("/api/incidents", payload);
  return response.data;
};

export const updateIncidentStatus = (id, status) =>
  api.patch(`/api/incidents/${id}/status`, { status });

export const createIncidentWithFile = async (formData) => {
  const response = await api.post("/api/incidents", formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });
  return response.data;
};
export const uploadAttachment = (id, formData) =>
  api.post(`/api/incidents/${id}/attachments`, formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });