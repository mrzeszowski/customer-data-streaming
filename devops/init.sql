create database customers_db;
create database products_db;
create database transactions_db;
create database shipments_db;

-- restart required
alter system set wal_level = logical;