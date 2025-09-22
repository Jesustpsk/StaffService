INSERT INTO passports (type, number)
VALUES (@type, @number)
    RETURNING id;