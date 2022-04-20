<?php

namespace App\Http\Controllers;

use App\Models\chuck_talks;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ChuckCantBeControlled extends Controller
{
    public function index()
    {
        return response()->json(chuck_talks::all(), 200);
    }

    public function randomChuck()
    {
        $randID = random_int(1,30);
        $randChuck = DB::table('chuck_talks')
            ->where('id', '=', $randID)
            ->value('frase');
        return response()->json($randChuck, 200);
    }
}
