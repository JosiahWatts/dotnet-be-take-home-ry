# Process

1. business rules

# Assumptions

1. Assuming calculations are (had a blast):

mileageTotal = (m * 0.12623) + m
weightTotal  = w × 3.29157

Have no idea if this is correct, it seemed to have worked out given the examples.

2. Assuming shipment line items can not be present without shipment stops.
3. Assuming somewhere there is an order object that is tested and complete
4. Assuming there will always be a weight/qty
5. Assuming pickup must always before dropoff, no circles?
6. Assuming first sequence will always have a distance of zero miles
7. Assuming that no stops, means clearing out the line items

# Shortcomings

0. Definitely better error handling. Most of these are just tossing exceptions
1. Api is not really where I'd want it to be
2. Data layer does not exist. Only using mock data.
3. Tests are terrible and I kind of rushed through them.
   1. For what it's worth, the domain layer is pretty well tested.
   2. Only tested on domain layer
   3. Lots of magic numbers....