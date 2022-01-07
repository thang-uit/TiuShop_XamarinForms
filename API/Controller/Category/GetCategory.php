<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Category.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$category = new Category($db->connect());

$arrayCategory = $category->getCategory();

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayCategory,
    "message" => "Get category name"
);

echo json_encode($array);
