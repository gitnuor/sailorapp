SELECT COLUMN_NAME,DATA_TYPE,
(
SELECT  COL_NAME(ic.OBJECT_ID,ic.column_id) 
FROM    sys.indexes AS i INNER JOIN 
        sys.index_columns AS ic ON  i.OBJECT_ID = ic.OBJECT_ID
                                AND i.index_id = ic.index_id
WHERE  
OBJECT_NAME(ic.OBJECT_ID)='Student' and COL_NAME(ic.OBJECT_ID,ic.column_id)=COLUMN_NAME
) as PK
,
(
SELECT c1.name from sys.objects o1
INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id
INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id
INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id
INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id
INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id 
AND fk.key_index_id = pk.unique_index_id
where o1.name='Student' and c1.name=COLUMN_NAME
) as FK_column
,
(
SELECT o2.name from sys.objects o1
INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id
INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id
INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id
INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id
INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id 
AND fk.key_index_id = pk.unique_index_id
where o1.name='Student' and c1.name=COLUMN_NAME
) as PK_table
,
(
SELECT c2.name from sys.objects o1
INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id
INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id
INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   
AND fkc.referenced_column_id = c2.column_id
INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id
INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id 
AND fk.key_index_id = pk.unique_index_id
where o1.name='Student' and c1.name=COLUMN_NAME
) as PK_column
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME =  'student'

--SELECT  i.name AS IndexName,
--        OBJECT_NAME(ic.OBJECT_ID) AS TableName,
--        COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName
--FROM    sys.indexes AS i INNER JOIN 
--        sys.index_columns AS ic ON  i.OBJECT_ID = ic.OBJECT_ID
--                                AND i.index_id = ic.index_id
--WHERE  
---- i.is_primary_key = 1
----and 
--OBJECT_NAME(ic.OBJECT_ID)='Student'


--SELECT c1.name AS FK_column, o2.name AS PK_table, c2.name AS PK_column FROM sys.objects o1
--INNER JOIN sys.foreign_keys fk  ON o1.object_id = fk.parent_object_id
--INNER JOIN sys.foreign_key_columns fkc  ON fk.object_id = fkc.constraint_object_id
--INNER JOIN sys.columns c1 ON fkc.parent_object_id = c1.object_id  AND fkc.parent_column_id = c1.column_id
--INNER JOIN sys.columns c2 ON fkc.referenced_object_id = c2.object_id   AND fkc.referenced_column_id = c2.column_id
--INNER JOIN sys.objects o2 ON fk.referenced_object_id = o2.object_id
--INNER JOIN sys.key_constraints pk  ON fk.referenced_object_id = pk.parent_object_id AND fk.key_index_id = pk.unique_index_id
--where o1.name='Student' 
--ORDER BY o1.name, o2.name, fkc.constraint_column_id

SELECT c.name 'Name', t.Name 'Type', c.max_length 'Length', 
                 ISNULL(i.is_primary_key, 0) 'PK' FROM    sys.columns c INNER JOIN  sys.types t ON c.user_type_id = t.user_type_id 
                 LEFT OUTER JOIN  sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN 
                sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('Student')
