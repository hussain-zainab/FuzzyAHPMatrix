# Fuzzy AHP Requirement Prioritization Tool

A decision-support application that implements the **Fuzzy Analytic Hierarchy Process (Fuzzy AHP)** to prioritize software requirements using pairwise comparison matrices from multiple stakeholders.

---

## Overview

Prioritizing software requirements often involves complex decision-making where different stakeholders have different opinions. Traditional **Analytic Hierarchy Process (AHP)** methods require extensive manual calculations that are time-consuming and prone to error.

This application simplifies the process by allowing users to **enter comparison weights directly**, after which the system automatically performs the **Fuzzy AHP calculations**, aggregates stakeholder opinions, and produces **normalized priority weights and final rankings**.

The tool supports multiple stakeholders such as **Developers, Analysts, and Faculty**, combines their judgments, and computes the final requirement priorities using fuzzy logic.

---

## Key Features

* Pairwise comparison matrices for multiple stakeholders
* Automatic reciprocal value handling
* Implementation of **Fuzzy AHP (Triangular Fuzzy Numbers)**
* Aggregation of stakeholder judgments
* Calculation of **L, M, U fuzzy values**
* Computation of **priority weights**
* Automatic **requirement ranking**
* Graphical user interface using **Windows Forms**
* Eliminates the need for manual matrix calculations

---

## Technologies Used

* **C#**
* **.NET Framework**
* **Windows Forms**
* **Matrix computation**
* **Fuzzy AHP algorithm**

---

## System Workflow

1. Users input pairwise comparison values for requirements using the **Saaty scale**.
2. The system converts comparison values into **Triangular Fuzzy Numbers (L, M, U)**.
3. Stakeholder matrices are aggregated into a final fuzzy matrix.
4. Synthetic extent values are calculated.
5. The system computes **normalized priority weights**.
6. Requirements are ranked automatically based on their priority scores.

---

## Application Interface

![Interface](Output-screenshots/Interface.png)

---

## Developer Matrix Input

![Developer Matrix](Output-screenshots/Developer%20Matrix.png)

---

## Analyst Matrix Input

![Analyst Matrix](Output-screenshots/Analyst%20Matrix.png)

---

## Faculty Matrix Input

![Faculty Matrix](Output-screenshots/Faculty%20Matrix.png)

---

## Fuzzy Matrix Calculations (L, M, U)

![LMU Calculations](Output-screenshots/L,M,U%20Calculations.png)

---

## Synthetic Analysis Values

![SA Values](Output-screenshots/SA%20Values.png)

---

## Priority Weights

![Priority Weights](Output-screenshots/Priority%20Weights.png)

---

## Final Ranking

![Ranking](Output-screenshots/Ranking.png)

---

## Why This Tool Is Useful

Manual Fuzzy AHP calculations involve multiple matrix operations and fuzzy number computations that can be tedious and error-prone.
This application allows users to **simply input comparison weights**, and the system automatically performs all calculations and produces the final prioritized results.

This makes the tool useful for:

* Software requirement prioritization
* Decision support systems
* Multi-criteria decision making
* Research and academic demonstrations of Fuzzy AHP

---

## Author

**Zainab Hussain**

B.Tech Mechanical & Automation Engineering
IGDTUW (Indira Gandhi Delhi Technical University for Women)
