# Graph3D

## Math

All vector variables are marked bold.

### Triangle and ray intersection math

Given:

**R<sub>0</sub>** - start point of the ray,

**Dir** - direction of the ray,

|**Dir**| = 1 - direction is normalized to be of length 1

**A**, **B**, **C** - the triangle points

#### Equations

Specific point **P** at ray may be expressed as:

**P** = **R<sub>0</sub>** + t * **Dir**

From another side specific **P** may be expressed in terms of triangle.
Let's introduce new coordinate system (**U**, **V**, **W**, **A**), where

**U** = **B** - **A**, first axis

**V** = **C** - **A**, second eaxis

**W** = **U** x **V**, third axis is calculated to be prependicular to both **U** and **V**. Dot product makes the job.

**A**, is our central point

Then point **P** in general case is equal:

**P** = **U** * u + **V** * v + **W** * w + **A**,

So what we need to find the intersection is just find these u, v, w

As our intersection must be in triangle plane we can say that

w = 0

We need to find u, v values. They may be also used as point to triangle texture material.

Let's introduce vector **D** which will be our point **P** but relative to our new coordinate system:

**D** = **P** - **A**

Or

**D<sub>x</sub>** = **U<sub>x</sub>** * u + **V<sub>x</sub>** * v + **W<sub>x</sub>** * w

**D<sub>y</sub>** = **U<sub>y</sub>** * u + **V<sub>y</sub>** * v + **W<sub>y</sub>** * w

**D<sub>z</sub>** = **U<sub>z</sub>** * u + **V<sub>z</sub>** * v + **W<sub>z</sub>** * w

These linear equations can be solved as

u = det<sub>u</sub> / det

v = det<sub>v</sub> / det

w = det<sub>w</sub> / det


|       |               |               |               |
|-------|---------------|---------------|---------------|
|       | U<sub>x</sub> | V<sub>x</sub> | W<sub>x</sub> |
| det = | U<sub>y</sub> | V<sub>y</sub> | W<sub>y</sub> |
|       | U<sub>z</sub> | V<sub>z</sub> | W<sub>z</sub> |


|                   |               |               |               |
|-------------------|---------------|---------------|---------------|
|                   | D<sub>x</sub> | V<sub>x</sub> | W<sub>x</sub> |
| det<sub>u</sub> = | D<sub>y</sub> | V<sub>y</sub> | W<sub>y</sub> |
|                   | D<sub>z</sub> | V<sub>z</sub> | W<sub>z</sub> |

|                   |               |               |               |
|-------------------|---------------|---------------|---------------|
|                   | U<sub>x</sub> | D<sub>x</sub> | W<sub>x</sub> |
| det<sub>v</sub> = | U<sub>y</sub> | D<sub>y</sub> | W<sub>y</sub> |
|                   | U<sub>z</sub> | D<sub>z</sub> | W<sub>z</sub> |

|                   |               |               |               |
|-------------------|---------------|---------------|---------------|
|                   | U<sub>x</sub> | V<sub>x</sub> | D<sub>x</sub> |
| det<sub>w</sub> = | U<sub>y</sub> | V<sub>y</sub> | D<sub>y</sub> |
|                   | U<sub>z</sub> | V<sub>z</sub> | D<sub>z</sub> |

Or

det<sub>u</sub> = **D** * (**V** x **W**)</p>
        
det<sub>v</sub> = **D** * (**W** x **U**)</p>
        
det<sub>w</sub> = **D** * (**U** x **V**)= **D** * **W**

u = det<sub>u</sub> / det = **D** * (**V** x **W**) / det = **D** * **T<sub>u</sub>**

v = det<sub>v</sub> / det = **D** * (**W** x **U**) / det = **D** * **T<sub>v</sub>**

w = det<sub>w</sub> / det = **D** * (**U** x **V**) / det = **D** * **T<sub>w</sub>**

w = 0

**D** * **T<sub>w</sub>** = 0

(**P** - **A**) * **T<sub>w</sub>** = 0

**P** * **T<sub>w</sub>** = **A** * **T<sub>w</sub>**

(**R<sub>0</sub>** + t * **Dir**) * **T<sub>w</sub>** = **A** * **T<sub>w</sub>**


**R<sub>0</sub>** * **T<sub>w</sub>** + t * **Dir** * **T<sub>w</sub>** = **A** * **T<sub>w</sub>**

t * **Dir** * **T<sub>w</sub>** = **A** * **T<sub>w</sub>** - **R<sub>0</sub>** * **T<sub>w</sub>**

t * **Dir** * **T<sub>w</sub>** = (**A** - **R<sub>0</sub>**) * **T<sub>w</sub>**

t = (**A** - **R<sub>0</sub>**) * **T<sub>w</sub>** / (**Dir** * **T<sub>w</sub>**)