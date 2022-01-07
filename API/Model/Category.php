<?php
class Category
{
    public $conn;

    public $categoryID = "categoryID";
    public $name = "name";

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function getCategory()
    {
        $query = "SELECT * FROM `category`";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        $arrayCategory = [];

        while ($row = $stmt->fetch()) {
            array_push($arrayCategory, array(
                $this->categoryID => $row["Cat_ID"],
                $this->name => $row["Cat_Name"],
            ));
        }
        return $arrayCategory;
    }
}
