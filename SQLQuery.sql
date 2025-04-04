CREATE TABLE TaxiTrips (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    tpep_pickup_datetime DATETIME NOT NULL,
    tpep_dropoff_datetime DATETIME NOT NULL,
    passenger_count INT NOT NULL,
    trip_distance FLOAT NOT NULL,
    store_and_fwd_flag VARCHAR(3) NOT NULL,
    PULocationID INT NOT NULL,
    DOLocationID INT NOT NULL,
    fare_amount DECIMAL(18, 2) NOT NULL,
    tip_amount DECIMAL(18, 2) NOT NULL
);