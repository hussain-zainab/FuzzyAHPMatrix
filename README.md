# Fuzzy AHP Requirement Prioritization Tool

A desktop application that implements the **Fuzzy Analytic Hierarchy Process (Fuzzy AHP)** to prioritize software requirements using pairwise comparison matrices from multiple stakeholders.

---

## Overview

Prioritizing software requirements often involves complex decision-making where multiple stakeholders have different opinions. Traditional **Analytic Hierarchy Process (AHP)** methods require manual matrix calculations that are time-consuming and prone to human error.

This application simplifies the process by allowing users to **enter comparison weights directly**, while the system automatically performs all **Fuzzy AHP computations**, aggregates stakeholder judgments, and produces **normalized priority weights and final rankings**.

The system supports inputs from different stakeholders such as:

* Developers
* Analysts
* Faculty

Their evaluations are combined using fuzzy logic to determine the **final priority of requirements**.

---

## Features

* Pairwise comparison matrices for multiple stakeholders
* Implementation of **Fuzzy AHP using triangular fuzzy numbers (L, M, U)**
* Automatic aggregation of stakeholder matrices
* Calculation of **synthetic analysis values**
* Computation of **normalized priority weights**
* Automatic **ranking of alternatives**
* Windows Forms graphical user interface
* Eliminates the need for **manual AHP matrix calculations**

---

## Technologies Used

* **C#**
* **.NET Framework**
* **Windows Forms**
* **Matrix computations**
* **Fuzzy AHP algorithm**

---

## System Workflow

1. Users input pairwise comparison values using the **Saaty scale**.
2. The system converts comparison values into **triangular fuzzy numbers (L, M, U)**.
3. Stakeholder matrices are aggregated into a single fuzzy matrix.
4. **Synthetic analysis values** are calculated.
5. The system computes **normalized priority weights**.
6. Alternatives are ranked automatically based on their priority scores.

---

## Application Interface
![Interface](Output-screenshots/Interface.png)

## Developer Matrix Input
![Developer Matrix](Output-screenshots/developer-matrix.png)

## Analyst Matrix Input
![Analyst Matrix](Output-screenshots/analyst-matrix.png)

## Faculty Matrix Input
![Faculty Matrix](Output-screenshots/faculty-matrix.png)

## Fuzzy Matrix Calculations
![LMU Calculations](Output-screenshots/lmu-calculations.png)

## Synthetic Analysis Values
![SA Values](Output-screenshots/sa-values.png)

## Priority Weights
![Priority Weights](Output-screenshots/priority-weights.png)

## Final Ranking
![Ranking](Output-screenshots/Ranking.png)

## Why This Tool Is Useful

Manual Fuzzy AHP calculations involve multiple matrix operations and fuzzy number computations that can be tedious and error-prone.

This application allows users to **simply input comparison weights**, and the system automatically performs all calculations and produces prioritized results.

This makes the tool useful for:

* Software requirement prioritization
* Multi-criteria decision making
* Decision support systems
* Research and academic demonstrations of Fuzzy AHP

---

## Author

**Zainab Hussain**
B.Tech Mechanical & Automation Engineering
IGDTUW – Indira Gandhi Delhi Technical University for Women
