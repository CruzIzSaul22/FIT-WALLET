namespace FITFINAL.Models
{
    public class Goal
    {
        // Identificador único
        public int Id { get; set; }

        // Nombre de la meta (ej: "Ahorro para laptop")
        public string Name { get; set; } = string.Empty;

        // Cantidad objetivo
        public decimal Target { get; set; }

        // Cantidad acumulada
        public decimal Progress { get; set; }

        // Indica si la meta ya se cumplió
        public bool IsCompleted => Progress >= Target;

        // Porcentaje de avance (útil para barras de progreso)
        public decimal Percentage =>
            Target > 0 ? (Progress / Target) : 0;
    }
}