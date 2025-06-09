# Planet Evaluation Algorithm

## Scoring Methodology

1. Each factor has:

   - A numeric value (converted from string)
   - A weight representing its importance

2. For each planet:
   Score = Σ(factorValue × factorWeight) / Σ(factorWeight)

3. The planet with the highest normalized score wins

## Example Calculation

Planet X has:

- Oxygen: 0.21 (weight 0.3)
- Water: 0.7 (weight 0.3)
- Temperature: 0.8 (weight 0.2)
- Threats: -0.2 (weight -0.5)

Calculation:
(0.21×0.3 + 0.7×0.3 + 0.8×0.2 + (-0.2×-0.5)) / (0.3+0.3+0.2+0.5) = 0.423
