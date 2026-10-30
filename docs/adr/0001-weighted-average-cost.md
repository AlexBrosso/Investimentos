# ADR-0001: Use weighted average cost for positions

- **Status:** Accepted
- **Date:** 2026-10-30

## Context

The app calculates each asset's position, average price and realized gains from the user's buy and sell trades. There are two common ways to assign a cost to the shares sold:

- **Weighted average cost:** all shares of the same asset form a single pool with one average price. A sale removes shares at that average, so the average price of the remaining shares does not change.
- **FIFO (first in, first out):** each buy is a separate lot, and a sale consumes the oldest lots first. The average price of the remaining shares changes after each sale.

Both methods produce the same total gain once the whole position is sold. They differ in *when* the gain is recognized. Example: buy 100 shares at R$ 30.00, buy 50 at R$ 36.00, then sell 30 at R$ 40.00:

| | Weighted average | FIFO |
|---|---|---|
| Cost of the 30 shares sold | R$ 960.00 | R$ 900.00 |
| Realized gain | R$ 240.00 | R$ 300.00 |
| Average price of the remaining 120 shares | R$ 32.00 | R$ 32.50 |

Forces at play:

- The app replaces the spreadsheet that investors use to prepare their income tax return. In Brazil, the Receita Federal requires the weighted average cost for stocks, FIIs and ETFs traded on B3.
- Shares of the same asset are fungible: B3 has no concept of selling "the shares bought in January".
## Decision

We will calculate positions using the **weighted average cost**:

- A buy updates the average price: `(current total cost + buy total) / (current quantity + bought quantity)`.
- A sale removes shares at the current average price. It changes the quantity and the total cost, never the average price.
- Realized gain = `quantity sold × (sale price − average price)`.

## Consequences

**Easier:**

- The realized gains shown in the app match what the user must report to the Receita Federal.
- The model is simpler: one pool per asset instead of a list of lots.

**Harder / given up:**

- Users cannot see gains per lot (FIFO or specific identification). Supporting that later would require storing lots and would be a new decision.
- The app is tied to the Brazilian rule. Supporting other markets with different tax rules would need another method.

**Follow-ups (out of scope here):**

- Rounding and precision of the average price (money is always `decimal`).
- Trading fees (brokerage, B3 fees) as part of the acquisition cost.
- Day trade, which has its own tax rules.

## References

- Receita Federal, [*Perguntas e Respostas IRPF 2026*](https://www.gov.br/receitafederal/pt-br/centrais-de-conteudo/publicacoes/perguntas-e-respostas/dirpf/p-r-irpf-2026-v1-00-2026-04-23.pdf), chapter "Renda variável", question "Mercado à vista – custo dos ativos": the acquisition cost is the weighted average of the unit costs ("média ponderada dos custos unitários").
- [Documenting Architecture Decisions](https://cognitect.com/blog/2011/11/15/documenting-architecture-decisions), Michael Nygard (ADR format)
