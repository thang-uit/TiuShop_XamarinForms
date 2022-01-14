<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Comment.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$comment = new Comment($db->connect());

$productID = isset($_POST["productID"]) && !empty($_POST["productID"]) ? $_POST["productID"] : "";
$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";
$rating = isset($_POST["rating"]) && !empty($_POST["rating"]) ? $_POST["rating"] : "";
$content = isset($_POST["content"]) && !empty($_POST["content"]) ? $_POST["content"] : "";

$status = $comment->InsertComment($productID, $userID, $rating, $content);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Add comment successfully!"
    );
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Add comment fail!"
    );
}

echo json_encode($array);
